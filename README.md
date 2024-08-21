## About the Author

This project was developed as part of a test by Roy Sefton 

### About Me

I am a software developer with experience in Microsoft technologies. 
Feel free to contact me for any inquiries or feedback.

Phil Rees has my contact details.

***

Related document : `Instructions.md`

Set as Startup Project:  `Smartwyre.DeveloperTest.Runner`

### Here's a summary of my refactoring process to adhere to SOLID principles:

### 1. **Identified the Problem**

- **Issue**: We needed to adapt different calculator implementations to a common interface while ensuring that the code adhered to SOLID principles.
- **Challenges**: 
  - Different calculators had varying method signatures.
  - Ensuring that adapters did not expose unused parameters or methods.
  - Handling nullability and ensuring proper exception handling.

### 2. **Applied SOLID Principles**

**Single Responsibility Principle (SRP)**

- **Adapter Classes**: Each adapter class (`AmountPerUomCalculatorAdapter`, `FixedCashAmountCalculatorAdapter`, `FixedRateRebateCalculatorAdapter`) was given the single responsibility of adapting a specific type of calculator to the `IRebateCalculator` interface.
- **Implementation**: Each adapter focuses on converting method calls from the `IRebateCalculator` interface to the methods of the underlying calculator, thereby maintaining a clear separation of concerns.

**Open/Closed Principle (OCP)**

- **Extension Without Modification**: New types of calculators can be supported by creating new adapter classes without modifying existing code.
- **Implementation**: The system was designed to allow the addition of new adapters for different calculators without changing the existing ones.

**Liskov Substitution Principle (LSP)**

- **Interchangeability**: The adapter classes were designed to ensure that instances of `IRebateCalculator` can be replaced with instances of any adapter without affecting the functionality.
- **Implementation**: Each adapter correctly implements the `IRebateCalculator` interface, fulfilling its contract and ensuring that it can be used interchangeably.

**Interface Segregation Principle (ISP)**

- **Minimal Interfaces**: The adapters implement only the methods required by `IRebateCalculator`, avoiding unnecessary dependencies.
- **Implementation**: The adapter methods (`IsApplicable`, `CalculateRebateAmount`) were tailored to the needs of the specific calculators, ensuring that no additional methods were exposed.

**Dependency Inversion Principle (DIP)**

- **Depend on Abstractions**: High-level modules like `RebateService` depend on the `IRebateCalculator` interface rather than concrete calculator implementations.
- **Implementation**: The `RebateService` was designed to work with the `IRebateCalculator` interface, and adapters were used to bridge the gap between the high-level module and the specific calculator implementations.

### 3. **Refactored Code**

**Adapter Classes**

- **`AmountPerUomCalculatorAdapter`**:
  - Adapts `IAmountPerUomCalculator` to `IRebateCalculator`.
  - Handles nullability and delegates method calls appropriately.
  - Ensures that unused parameters are not passed to the underlying calculator.

- **`FixedCashAmountCalculatorAdapter`**:
  - Adapts `IFixedCashAmountCalculator` to `IRebateCalculator`.
  - Handles nullability for used parameters and provides default values for unused ones.
  - Ensures that only necessary parameters are validated and used.

- **`FixedRateRebateCalculatorAdapter`**:
  - Adapts `IFixedRateRebateCalculator` to `IRebateCalculator`.
  - Validates all parameters as required and delegates method calls correctly.

**Usage**

- **In `Program.cs`**:
  - Used the adapters to integrate different calculator implementations into a common interface, making the system extensible and maintainable.

### Summary

By refactoring the code to use the adapter pattern and ensuring adherence to SOLID principles, we achieved:
- **Modularity**: Clear separation of concerns with single-responsibility classes.
- **Extensibility**: Ability to add new calculator types without modifying existing code.
- **Interchangeability**: Flexibility to replace or add new calculators without affecting the overall system.
- **Minimal Dependencies**: Avoiding unnecessary dependencies and methods.
- **Abstraction**: High-level components depend on abstractions rather than concrete implementations.

This refactoring ensures that the codebase is robust, flexible, and easier to maintain.
