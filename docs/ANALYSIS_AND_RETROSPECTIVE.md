# Анализ и ретроспектива проекта ExchangeTracker

## 1. Обзор структуры

| Проект | Назначение |
|--------|------------|
| **ExchangeTracker.Gateway** | Web API (точка входа) |
| **ExchangeTracker.Core** | Операции, маппинг Core |
| **Core.Abstractions** | Result, Error, модели, интерфейсы операций |
| **ExchangeClients.Abstractions** | IExchangeClient, модели бирж, опции |
| **BinanceExchangeClient** | HTTP-клиент Binance |
| **BybitExchangeClient** | Клиент Bybit (пока не реализован) |
| **ExchangeTracker.DAL** | Заглушка, в Gateway не используется |
| **ExchangeTracker.Tests** | xUnit-тесты |

---

## 2. Проблемы в коде

### 2.1 Обработка HTTP в Binance-клиенте

**Файл:** `Implementations/Exchanges/BinanceExchangeClient/BinanceExchangeClient.cs`

- Не проверяется `response.IsSuccessStatusCode` / не вызывается `EnsureSuccessStatusCode()`.
- При 4xx/5xx от Binance (например, неверный символ) чтение JSON может упасть или вернуть не тот тип → исключение → 500.
- Сетевые ошибки уходят как 500 без разделения на «биржа недоступна» / «таймаут».

**Рекомендация:** Проверять статус ответа, при 4xx возвращать осмысленную ошибку (например, 404 для несуществующего тикера), при 5xx/сети — зафиксировать в логах и вернуть единообразный ответ (например, 502/503).

### 2.2 Неверный формат ответа в ResponseExtensions

**Файл:** `ExchangeTracker.Gateway/Extensions/ResponseExtensions.cs`

```csharp
var response = new JsonResult(new { error = error.Message });
return new ObjectResult(response) { StatusCode = statusCode };
```

В `ObjectResult` в качестве `Value` передаётся `JsonResult`. В pipeline может сериализоваться сам объект `JsonResult`, а не тело `{ "error": "..." }`.

**Рекомендация:** Возвращать тело напрямую:

```csharp
return new ObjectResult(new { error = error.Message }) { StatusCode = statusCode };
```

### 2.3 Result в операциях не используется

**Файл:** `Implementations/ExchangeTracker.Core/Operations/GetExchangePairPriceQueryOperation.cs`

Операция объявлена как `Task<Result<PairPriceQueryOperationModel>>`, но при ошибках только бросает исключения (нет `return Error.NotFound(...)` и т.п.). Контроллер обрабатывает `result.IsFailure` и вызывает `ToResponse()`, но этот путь никогда не выполняется — все ошибки идут через `ApiExceptionFilter`.

**Рекомендация:** Либо явно возвращать `Result` с `Error` (например, «биржа не найдена», «тикер не найден») и обрабатывать в контроллере, либо упростить сигнатуру до «успех или исключение» и не полагаться на `Result` в API.

### 2.4 Нет валидации входящих данных

- **PriceDto:** нет атрибутов `[Required]`, ограничений длины/формата. Пустые или null-строки доходят до операции.
- **Операция:** не проверяет пустые `PairName` / `ExchangeName` перед вызовом `GetRequiredKeyedService` и клиента.
- **Опции:** не проверяется наличие/корректность `BinanceUrl` при старте.

**Рекомендация:** Добавить валидацию (DataAnnotations на DTO, FluentValidation или проверки в операции) и возвращать 400 с понятным сообщением.

### 2.5 Bybit зарегистрирован, но не реализован

Вызов метода для биржи «Bybit» всегда приводит к `NotImplementedException` → 500. Пользователь не отличает «биржа пока не поддерживается» от внутренней ошибки.

**Рекомендация:** Либо не регистрировать Bybit до реализации, либо возвращать явную ошибку (например, 501 Not Implemented или кастомный код/сообщение).

### 2.6 DAL не используется

Gateway ссылается на `ExchangeTracker.DAL`, проекты DAL и DAL.Abstractions — заглушки. Это создаёт лишнюю зависимость и путаницу.

**Рекомендация:** Убрать ссылку из Gateway до появления реальной работы с БД или явно пометить как «зарезервировано под БД».

---

## 3. Маппинг (AutoMapper)

### Что есть

| Профиль | Маппинг |
|---------|---------|
| **GatewayModelsMappingProfile** | `PriceDto` → `GetPairPriceQueryOperationModel` |
| **CoreModelsMappingProfile** | `GetPairPriceQueryOperationModel` → `GetPriceExchangeModel`, `PriceExchangeModel` → `PairPriceQueryOperationModel` |
| **BinanceMappingProfile** | `PriceExchangeResponseModel` → `PriceExchangeModel` |

### Что может понадобиться

- **Bybit:** когда будет реализован клиент — свой response-тип и профиль (аналог Binance).
- **Ответ API:** сейчас контроллер возвращает голый `decimal`. Если появится DTO ответа (например, `{ "price": ..., "exchange": ... }`) — нужен маппинг из `PairPriceQueryOperationModel` (или из модели ответа биржи) в этот DTO.

На текущий сценарий маппингов достаточно, явных «забытых» маппингов нет.

---

## 4. Регистрации (DI)

### Что зарегистрировано

- **Gateway (Program.cs):** опции, контроллеры, Swagger, маппер, Core, Binance и Bybit (ключи `"Binance"` / `"Bybit"`).
- **Core:** `IGetExchangePairPriceQueryOperation` → `GetExchangePairPriceQueryOperation` (Scoped).
- **Binance:** `AddHttpClient()`, ключевой `IExchangeClient` (Scoped).
- **Bybit:** только ключевой `IExchangeClient` (Scoped), без HttpClient и без использования опций.

### Пропуски / замечания

- **Bybit:** при реализации понадобятся `AddHttpClient()` и привязка опций (BybitUrl), как у Binance.
- **HttpClient для Binance:** используется общий фабричный клиент без имени; нет настроек таймаута, retry (Polly) — при необходимости их стоит добавить.
- **Валидация опций:** `ExchangeClientOptions` не валидируются при старте (например, через `ValidateDataAnnotations()` или кастомный IValidateOptions).

---

## 5. Тесты — что есть и что добавить

### Сейчас покрыто

- **GetExchangePairPriceQueryOperation:** успех для Binance (BTCUSDT, ETHUSDT), неверное имя биржи, несуществующий тикер, пустой PairName.
- **Mapper:** одна проверка валидности конфигурации маппера (через фикстуру).

### Что стоит добавить

| Область | Что тестировать |
|--------|------------------|
| **Контроллер** | `PriceController`: успешный ответ 200 и тело (decimal), ответ при неверном/пустом query (400), при несуществующей бирже и при несуществующем тикере (ожидаемые коды и формат `{ "error": "..." }`). |
| **ApiExceptionFilter** | Соответствие типа исключения и HTTP-кода (400, 401, 404, 408, 409, 500) и формат JSON-ответа. |
| **ResponseExtensions** | Для каждого `ErrorType`: код (400, 404, 409, 500) и тело `{ "error": "<message>" }`. |
| **Валидация** | После появления валидации на DTO/модели — тесты на 400 при пустых/некорректных PairName, ExchangeName. |
| **Операция + Result** | Если операция начнёт возвращать `Error`: тесты на `IsFailure` и корректный `Error.Type`/Message. |
| **Binance-клиент (изолированно)** | Мок HTTP: успешный ответ, 404/400 от API (неверный символ), таймаут/сетевая ошибка — ожидаемое исключение или (в будущем) Result. |
| **Маппинг** | Отдельные тесты на ключевые пары: PriceDto → GetPairPriceQueryOperationModel, PriceExchangeResponseModel → PriceExchangeModel (на конкретных полях). |

---

## 6. HTTP-статусы и валидация ответов API

### Текущее поведение

- **Успех:** 200, тело — одно число (цена).
- **Ошибки:** через `ApiExceptionFilter`: 400 (ArgumentException, InvalidOperationException), 401, 404, 408, 409, 500.
- **Result (пока не используется):** в `ResponseExtensions` заложены 400, 404, 409, 500 по типу `Error`.

### Что имеет смысл добавить / уточнить

| Ситуация | Текущий код | Рекомендация |
|----------|-------------|--------------|
| Неверный или пустой query (пара, биржа) | Исключения → 400 | Явная валидация и **400** с телом `{ "error": "..." }` (или список ошибок валидации). |
| Биржа не найдена (неверное имя) | InvalidOperationException → **400** | Трактовка как **404** («Exchange not found») и сообщение в теле. |
| Тикер не найден на бирже | Исключение из клиента → **500** | В клиенте обрабатывать 404/400 от Binance и возвращать **404** с сообщением «Symbol not found» (или через Result). |
| Биржа временно недоступна / таймаут | Исключение → **500** | Оставить 500 или ввести **502/503** с единым форматом ответа. |
| Метод не реализован (Bybit) | NotImplementedException → **500** | **501 Not Implemented** и сообщение «Exchange not yet supported». |
| Внутренняя ошибка приложения | **500** | Оставить 500, в теле — общее сообщение, детали только в логах. |

### Единый формат ошибок

Сейчас и фильтр, и (после правки) расширение отдают что-то вроде `{ "error": "<message>" }`. Имеет смысл зафиксировать это в контракте и при необходимости добавить поле `code` (например, `Validation`, `NotFound`) для удобства клиентов.

---

## 7. Сводная оценка

| Категория | Оценка | Комментарий |
|-----------|--------|-------------|
| **Структура решения** | Хорошо | Разделение Gateway / Core / Abstractions / клиенты бирж понятное. |
| **Маппинг** | Хорошо | Все текущие сценарии покрыты; для Bybit и ответа API — добавить по мере появления типов. |
| **DI** | Нормально | Регистрации достаточны; Bybit и опции/HttpClient можно донастроить при реализации. |
| **Обработка ошибок** | Требует доработки | Нет проверки HTTP в Binance, Result не используется, возможная ошибка в ResponseExtensions, смешение 400/404/500. |
| **Валидация** | Слабо | Нет проверки DTO и опций. |
| **Тесты** | Частично | Операция и маппер частично покрыты; нет тестов контроллера, фильтра, расширений, валидации и изолированного клиента. |
| **API-контракт** | Нормально | Успех и ошибки возвращаются; стоит зафиксировать форматы и коды (в т.ч. 404, 501) и поправить ResponseExtensions. |

Приоритеты: (1) исправление `ResponseExtensions` и проверка HTTP в Binance, (2) валидация входящих данных и явные коды 400/404/501, (3) тесты контроллера и фильтра, (4) решение по использованию Result в операциях и ответам API.
