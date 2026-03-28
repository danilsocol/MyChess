# План доработки и переработки тестов по ТЗ

Документ основан на сверке `docs/test-spec-core.md` с текущими классами в `ChessGame.Tests` (состояние на момент составления).

**Статус структуры (тесты):** классы приведены к разделам ТЗ (см. таблицу ниже). Файл `TzMissingCoverageTests` и дублирующие классы удалены; сценарии перенесены в именованные по § ТЗ классы. Полный аудит смысла кейсов vs формулировок ТЗ — см. разделы 2–5 ниже.

## 1. Целевая структура классов (как в ТЗ)

Рекомендуется привести имена классов к разделам ТЗ один-к-одному:

| Раздел ТЗ | Целевой класс тестов |
|-----------|----------------------|
| §1 Инициализация | `GameEngineInitializationTests` |
| §2 Валидация | `GameEngineInputValidationTests` |
| §3 Пешка | `PawnMoveGenerationTests` |
| §3 Конь | `KnightMoveGenerationTests` |
| §3 Слон | `BishopMoveGenerationTests` |
| §3 Ладья | `RookMoveGenerationTests` |
| §3 Ферзь | `QueenMoveGenerationTests` |
| §3 Король | `KingMoveGenerationTests` |
| §4 Рокировка | `CastlingRulesTests` |
| §4 En passant | `EnPassantRulesTests` |
| §4 Превращение | `PawnPromotionTests` |
| §5 Шах / мат / пат | `CheckMateStalemateTests` |
| §6 Ничьи | `DrawRulesTests` |
| §7 MoveStatus | `MoveStatusTests` |
| §8 История / отмена | `GameEngineHistoryTests` |
| §9 Контракт GetPossibleMoves | `PossibleMovesContractTests` |
| §10 Сохранение / экспорт | `GamePersistenceTests` |
| §11 Устойчивость | `GameEngineRobustnessTests` |
| Вспомогательное | `RulesValidatorTests` (если тестируется отдельно от `IGameEngine`) |

Сейчас часть разделов размазана по «техническим» именам (`GameEngineStateTransitionTests`, `GameEngineContractAndPawnRulesTests`, `GameEngineStateContractTests`, `GameEngineCaptureRulesTests`, `GameEngineEdgeCaseTests`) и одному большому `TzMissingCoverageTests`.

---

## 2. Текущее состояние: что где лежит

### §1 Инициализация

| ID | Где сейчас | Замечание |
|----|------------|-----------|
| CORE-INIT-001 | `TzMissingCoverageTests.CORE_INIT_001` | Слабая проверка: размер поля и не-null ячеек, **не** полная стартовая расстановка по правилам шахмат. |
| CORE-INIT-002 | `GameEngineInitializationTests` | Ок по смыслу. |
| CORE-INIT-003 | `GameEngineInitializationTests` | Ок. |
| CORE-INIT-004 | `GameEngineInitializationTests`, `GameEngineStateContractTests` | Дублирование смысла экспорта — можно оставить один класс. |
| CORE-INIT-005 | `GameEngineStateContractTests` (Same instance) | ТЗ: «не изменяют состояние» — частично: проверена стабильность ссылок, **нет** явного сценария «вызвали GetBoard, изменили доску снаружи / через ход — инварианты». |

### §2 Валидация

| ID | Где сейчас | Замечание |
|----|------------|-----------|
| CORE-VAL-001 | `GameEngineContractAndPawnRulesTests` | Ок по размещению (ближе к контракту движка). |
| CORE-VAL-002 … 005 | `GameEngineMoveValidationTests` | Ок. |
| CORE-VAL-006 | **`GameEngineEdgeCaseTests` и `TzMissingCoverageTests`** | **Дубликат** — один тест нужно удалить или оставить один класс. |
| CORE-VAL-007 | **То же** | **Дубликат.** |

Дополнительно: в `GameEngineContractAndPawnRulesTests` лежат сценарии, ближе к §7 (невалидный ход не меняет доску/очередь) — логичнее перенести в `MoveStatusTests` или §2.

### §3 Ходы фигур

| Подраздел | Где сейчас | Замечание |
|-----------|------------|-----------|
| Пешка | `PawnMoveGenerationTests` + `GameEngineContractAndPawnRulesTests` | Разбито корректно по идее, но **нет явных имён методов с ID ТЗ**; CORE-PAWN-001/002 смешаны в одном тесте «1 и 2 шага». |
| Конь | `KnightMoveGenerationTests` + `TzMissingCoverageTests.CORE_KNIGHT_003` | Негативный кейс коня в «свалке» ТЗ — **перенести** в `KnightMoveGenerationTests`. |
| Слон | `BishopMoveGenerationTests` + `TzMissingCoverageTests.CORE_BISHOP_003` | Аналогично — **перенести** neg в `BishopMoveGenerationTests`. |
| Ладья | `RookMoveGenerationTests` + `TzMissingCoverageTests.CORE_ROOK_003` | Аналогично. |
| Ферзь | `QueenMoveGenerationTests` | Нет отдельного теста **CORE-QUEEN-002** (блокировка дальнего хода при фигуре на луче) — только смежные сценарии со взятием. |
| Король | `KingMoveGenerationTests` + `TzMissingCoverageTests.CORE_KING_001` | **Дублирование** смысла с `KingMoveGenerationTests`. **CORE-KING-002, CORE-KING-003** в коде **отсутствуют**. |

### §4 Спецправила

| ID | Где сейчас | Замечание |
|----|------------|-----------|
| CORE-CASTLE-001 … 002 | `TzMissingCoverageTests` | Проверяют лишь «ход не Success», **не** успешную рокировку по ТЗ. **CORE-CASTLE-003 … 008 отсутствуют.** |
| CORE-ENPASSANT-001 | `TzMissingCoverageTests` | Не соответствует ТЗ (нет сценария после двойного хода пешки). **CORE-ENPASSANT-002 отсутствует.** |
| CORE-PROMO-001 | `TzMissingCoverageTests` | Есть ход с `MoveType.Promotion`, но **нет смены фигуры** на доске по смыслу ТЗ. **CORE-PROMO-002, 003 отсутствуют.** |

### §5 Шах, мат, пат

| ID | Где сейчас | Замечание |
|----|------------|-----------|
| CORE-CHECK-001 | `TzMissingCoverageTests` + `RulesValidatorTests` | Дублирование идеи «шах детектится»; ТЗ про **фиксацию после хода** (`MakeMove` / состояние) **не покрыто** отдельно. |
| CORE-CHECK-002 | `TzMissingCoverageTests` | По ТЗ нужны **только ходы, снимающие шах** у `GetPossibleMoves` движка; сейчас проверка **не про это** (король без шаха, NotEmpty). |
| CORE-CHECK-003 | `TzMissingCoverageTests` | Семантика не совпадает с ТЗ («двойной шах → только король»); завязка на текущий баг/особенность `IsCheckmate`. |
| CORE-MATE-001 | `TzMissingCoverageTests` + `RulesValidatorTests` | Слабые/разрозненные проверки; нет связки с **окончанием партии** в `GameEngine` / `MoveStatus`. |
| CORE-STALE-001 | `TzMissingCoverageTests` | Только `ExportState().IsStalemate == false` — **не** тест патовой позиции. |

### §6 Ничьи

Все **CORE-DRAW-001 … 005** сейчас в `TzMissingCoverageTests` сведены к проверке «флаг stalemate false» — **не** реализуют сценарии ТЗ (материал, повтор, 50 ходов).

### §7 MoveStatus

| ID | Где сейчас | Замечание |
|----|------------|-----------|
| CORE-STATUS-001 | `GameEngineStateTransitionTests` | Ок по смыслу Success. |
| CORE-STATUS-002 | `GameEngineMoveValidationTests` | Частично (разные Invalid). |
| CORE-STATUS-003, 004 | `TzMissingCoverageTests` | **Формально слабые/искусственные** (проверки enum), не поведение движка. |
| CORE-STATUS-005 | `GameEngineContractAndPawnRulesTests` | Ок; лучше вынести в `MoveStatusTests`. |
| CannotCaptureOwn | `GameEngineCaptureRulesTests` | В ТЗ нет отдельного ID, но полезно; можно оставить в §2 или §7 с комментарием. |

### §8 История и отмена

| ID | Где сейчас | Замечание |
|----|------------|-----------|
| CORE-HIST-001, 002, 006 | `GameEngineStateTransitionTests` | Ок. |
| CORE-HIST-003 … 005 | `TzMissingCoverageTests` | По ТЗ нужен **откат** рокировки / en passant / превращения; сейчас в основном «история не растёт при неуспехе» или «+1 при promo» — **не** то же самое. |

Дополнительно: `GameEngineStateContractTests.MakeMove_Capture_SavesTakenFigureInHistory` — логично в §8.

### §9 Контракт GetPossibleMoves

| ID | Где сейчас | Замечание |
|----|------------|-----------|
| CORE-MOVES-001 | `TzMissingCoverageTests` | Слабо (не null). |
| CORE-MOVES-002 | `TzMissingCoverageTests` | **Не** про исключение ходов, оставляющих короля под шахом. |
| CORE-MOVES-003 | `TzMissingCoverageTests` | **Не** про pin. |
| CORE-MOVES-004, 005 | `TzMissingCoverageTests` | Дубликаты по смыслу уникальности/стабильности — ок как задел, но без привязки к легальности. |

### §10 Сохранение / загрузка / экспорт

| ID | Где сейчас | Замечание |
|----|------------|-----------|
| CORE-STATE-001 | `TzMissingCoverageTests` + дубль в `GameEngineContractAndPawnRulesTests.SaveAndDownloadGame` | **Дублирование** NotImplemented. |
| CORE-STATE-002 … 005 | `TzMissingCoverageTests` + `GameEngineStateContractTests` | Частично пересекается; **CORE-STATE-002/003** по ТЗ (история после загрузки, права рокировки/en passant) **не реализованы** из-за отсутствия Save/Load. |
| GameOver | `GameEngineContractAndPawnRulesTests` | Нет ID в ТЗ, но полезно; перенести в `GamePersistenceTests`. |

### §11 Устойчивость

| ID | Где сейчас | Замечание |
|----|------------|-----------|
| CORE-ROBUST-001 | `TzMissingCoverageTests` | Серия **невалидных** ходов, тогда как в ТЗ — «длинная серия **валидных**». |
| CORE-ROBUST-002, 003 | `TzMissingCoverageTests` | Частично ок; ROBUST-003 короткая партия, не «длинная». |

### RulesValidator

`RulesValidatorTests` — не раздел ТЗ напрямую; уместно как модульные тесты вспомогательной логики **или** перенести сценарии в §5 после того, как движок будет проксировать шах/мат через публичный API.

---

## 3. Критические проблемы структуры

1. **`TzMissingCoverageTests`** — «монолит»: смешаны ID из разных § ТЗ, часть проверок **не соответствует** формулировкам в `test-spec-core.md`.
2. **Дубликаты**: VAL-006/007; Save/Download; частично CHECK; KING-001; INIT-004.
3. **Нет классов** под §4 (отдельно), §6, полноценный §5/§7 — всё размазано или заглушки.
4. **Имена тестов**: в большинстве файлов нет префикса `CORE-xxx`, сложно проводить аудит по ТЗ.

---

## 4. План переработки (по этапам)

### Этап A — структура без изменения логики тестов

1. Создать пустые/скелетные классы по таблице из §1 этого документа.
2. Переместить методы из `TzMissingCoverageTests` в соответствующие классы **по разделу ТЗ** (переименовать методы в `CORE_XXX_YYY_Description` для трассируемости).
3. Удалить дубликаты VAL-006/007 и Save/Download (оставить один класс — предпочтительно §2 и §10).
4. Перенести `GameEngineCaptureRulesTests` в §2 или §7 с явной ссылкой на ТЗ в комментарии.

### Этап B — выровнять смысл тестов под ТЗ

1. **INIT-001**: заменить на проверку стартовой позиции (количество и типы фигур, координаты) или пометить как отдельный технический тест и добавить полноценный INIT-001.
2. **PAWN-001 / 002**: разнести на два теста или `[Theory]` с явными ID в display name.
3. **KING-002, KING-003**: добавить позиции с угрозой и с псевдо-легальными ходами, оставляющими короля под шах (после доработки ядра).
4. **QUEEN-002**: добавить блокировку на луче.
5. **§4–§6**: либо реализовать в ядре и написать настоящие тесты, либо временно **не** использовать ID из ТЗ для заглушек (отдельный список `KnownGap`), чтобы не вводить в заблуждение.

### Этап C — критерии готовности из ТЗ

1. Для каждого метода `IGameEngine` — явная матрица: позитивный + негативный тест в классе §10/§2/§8 и т.д.
2. Все **P0** из ТЗ должны иметь проверку, совпадающую с формулировкой (пройти ревью по `test-spec-core.md`).
3. Добавить `docs/test-coverage-matrix.md` (опционально): таблица `CORE-ID → класс → имя метода → статус (ok / gap / wrong)`.

### Этап D — инфраструктура

1. Расширить `TestDataFactory`: фабрики типовых позиций (шах, мат, пат, рокировка, en passant).
2. Ввести общий helper сравнения состояния партии (для HIST-006 и STATE-* после реализации сохранения).

---

## 5. Краткий чеклист «чего не хватает» (по ID ТЗ)

- **CORE-INIT-001** (полная стартовая расстановка), **CORE-INIT-005** (явно про отсутствие побочных эффектов от геттеров).
- **CORE-KING-002, CORE-KING-003**
- **CORE-QUEEN-002** (явная блокировка)
- **CORE-CASTLE-003 … 008**, успешные **CORE-CASTLE-001/002** по правилам
- **CORE-ENPASSANT-001/002** по правилам
- **CORE-PROMO-002, CORE-PROMO-003**
- **CORE-CHECK-002** (фильтрация ходов при шахе в API движка), **CORE-CHECK-003** по смыслу ТЗ
- **CORE-MATE-001**, **CORE-STALE-001** с реальными позициями и ожиданиями от движка
- **CORE-DRAW-001 … 005** как сценарии, а не проверка `IsStalemate == false`
- **CORE-STATUS-003, 004** как поведение `MakeMove` / `ExportState`, а не формальности enum
- **CORE-HIST-003 … 005** как откат спецходов
- **CORE-MOVES-001 … 003** как легальность и pin
- **CORE-STATE-002, 003** после реализации Save/Load
- **CORE-ROBUST-001** — серия валидных ходов

---

## 6. Итог

Сейчас тесты **функционально покрывают** базовый движок, пешку, часть фигур и валидацию, но **структура классов не совпадает** с разделами ТЗ, а файл **`TzMissingCoverageTests` концентрирует несоответствия** между ID ТЗ и реальной проверкой. Рекомендуется разобрать его по классам из §1 плана, устранить дубликаты и затем итеративно заменить «заглушечные» CORE-тесты на сценарии, совпадающие с текстом `test-spec-core.md`.
