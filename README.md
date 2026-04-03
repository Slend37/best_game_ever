# Игра в змейку

## Концепция

Консольная игра в "змейку" с мультиплеером и возможностью прохождения уровней различной сложности.

## Управление

*В разработке*

## Тестирование

Для тестирования создан проект `BestGameEver.Tests` на `xUnit`.
Объект тестирования: класс `Snake`

### Запуск

```
cd .\BestGameEver.Tests\
dotnet test
```

или из корня

```
dotnet test BestGameEver.Tests\BestGameEver.Tests.csproj
```

### Реализованные тесты

1. `Constructor_WithValidParameters_SetsAllProperties`
   Позитивный сценарий. Проверяет, что объект `Snake` корректно создается с валидными параметрами и все свойства получают ожидаемые значения.
2. `Move_Up_ChangesPositionAndDirection`
   Позитивный сценарий. Проверяет, что при движении вверх у змейки изменяются координаты и текущее направление.
3. `Constructor_SizeLessThanThree_ThrowsArgumentException`
   Негативный сценарий. Проверяет, что при попытке создать змейку с размером меньше 3 выбрасывается исключение `ArgumentException`.
4. `Constructor_WinSizeLessThanSize_ThrowsArgumentException`
   Негативный сценарий. Проверяет, что нельзя создать змейку, если размер для победы меньше текущего размера змейки.
5. `Move_ShouldNotAllowImmediateReverseDirection_IntentionallyFailsUntilRuleIsImplemented`
   Граничный сценарий. Проверяет игровое правило, по которому змейка не должна мгновенно разворачиваться в противоположную сторону. На текущем этапе этот тест специально не проходит, так как такая проверка в логике `Snake` пока не реализована.

### Результат запуска тестов

Текущий результат запуска:

```powershell
PS C:\Users\Aleksandr_Tuman\Documents\GitHub\best_game_ever\BestGameEver.Tests> dotnet test
Восстановление завершено (1,3 с)
  BestGameEver net10.0 успешно выполнено (0,5 с) → C:\Users\Aleksandr_Tuman\Documents\GitHub\best_game_ever\BestGameEver\bin\Debug\net10.0\BestGameEver.dll  BestGameEver.Tests net10.0 успешно выполнено (0,4 с) → bin\Debug\net10.0\BestGameEver.Tests.dll
[xUnit.net 00:00:00.00] xUnit.net VSTest Adapter v3.1.4+50e68bbb8b (64-bit .NET 10.0.5)
[xUnit.net 00:00:00.31]   Discovering: BestGameEver.Tests
[xUnit.net 00:00:00.45]   Discovered:  BestGameEver.Tests
[xUnit.net 00:00:00.50]   Starting:    BestGameEver.Tests
[xUnit.net 00:00:00.63]     BestGameEver.Tests.SnakeTests.Move_ShouldNotAllowImmediateReverseDirection_IntentionallyFailsUntilRuleIsImplemented [FAIL]
[xUnit.net 00:00:00.64]       Assert.Equal() Failure: Values differ
[xUnit.net 00:00:00.64]       Expected: Right
[xUnit.net 00:00:00.64]       Actual:   Left
[xUnit.net 00:00:00.64]       Stack Trace:
[xUnit.net 00:00:00.64]         C:\Users\Aleksandr_Tuman\Documents\GitHub\best_game_ever\BestGameEver.Tests\SnakeTests.cs(54,0): at BestGameEver.Tests.SnakeTests.Move_ShouldNotAllowImmediateReverseDirection_IntentionallyFailsUntilRuleIsImplemented()
[xUnit.net 00:00:00.64]            at System.Reflection.MethodBaseInvoker.InterpretedInvoke_Method(Object obj, IntPtr* args)
[xUnit.net 00:00:00.64]            at System.Reflection.MethodBaseInvoker.InvokeWithNoArgs(Object obj, BindingFlags invokeAttr)
[xUnit.net 00:00:00.65]   Finished:    BestGameEver.Tests  BestGameEver.Tests (тест) net10.0 сбой с ошибками (1) (2,7 с)
    C:\Users\Aleksandr_Tuman\Documents\GitHub\best_game_ever\BestGameEver.Tests\SnakeTests.cs(54): error TESTERROR:
      BestGameEver.Tests.SnakeTests.Move_ShouldNotAllowImmediateReverseDirection_IntentionallyFailsUntilRuleIsImplemented (3ms): Сообщение об ошибке: Assert.Equ
      al() Failure: Values differ
      Expected: Right
      Actual:   Left
      Трассировка стека:
         at BestGameEver.Tests.SnakeTests.Move_ShouldNotAllowImmediateReverseDirection_IntentionallyFailsUntilRuleIsImplemented() in C:\Users\Aleksandr_Tuman\Do
      cuments\GitHub\best_game_ever\BestGameEver.Tests\SnakeTests.cs:line 54
         at System.Reflection.MethodBaseInvoker.InterpretedInvoke_Method(Object obj, IntPtr* args)
         at System.Reflection.MethodBaseInvoker.InvokeWithNoArgs(Object obj, BindingFlags invokeAttr)Сводка теста: всего: 5; сбой: 1; успешно: 4; пропущено: 0; длительность: 2,7 с
Сборка сбой с ошибками (1) через 6,4 с
```


- позитивные и негативные сценарии для класса `Snake` покрыты
- один тест намеренно оставлен падающим

## Паттерн Facade

В рамках задания №8 был использован структурный паттерн **Facade**.

### Причина выбора
В проекте уже существовал игровой цикл, состоящий из нескольких последовательных шагов:
- обработка ввода
- обновление состояния игры
- отрисовка

Чтобы упростить код и скрыть порядок вызова этих методов, был создан класс `GameLoopFacade`, предоставляющий единый метод `Run()`  
В результате код был упрощён: вместо нескольких вызовов используется один метод `Run()`
