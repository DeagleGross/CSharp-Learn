# The Common Type System
CLR is all about the types. Because types are at the root of CLR, Microsoft has created a formal specification - the **Common Type System (CTS)** - that describes how types are defined and how they behave.

The CTS specification states that a type contain zero or more members. There are 4 different members which a type can incapsulate:
- **Field** - a data variable that shows a state of object
- **Method** - a function to perform an operation on the object. Usually, it changes state of object.
- **Property** - one or two methods that contain logic and expose a data as a field. It can look like a field to a caller.
- **Event** - allows a notification mechanism between an object and other interested object.

CTS specifies rules of visibility in some special keywords that modify type visibility:
- **Private** - the member is accessible only by other members in the same class type
- **Family** - the member is accessible by derived types, regardless of whether they are within the same assembly.
- **Family and assembly** - family and only if types are in the same assembly
- **Assembly** - the member is accessible by any code in the same assembly.
- **Family or assembly** - the member is accessible by derived types in any assembly.
- **Public** - the member is accessible by any code in any assembly.

Some programming languages allow more features to use than CLR. I.e. in C++ one can derive from multiple classes, but compiler will report an error because CLR can't accept such type.

Every type in CLR inherits from a predefined type `System.Object`. It has several methods implemented:
- Compare two instances for equality
- Obtain a hash code for the instance
- Query the true type of an instance
- Perform a shallow (bitwise) copy of the instance
- Obtain a string representation of the instance object's current state.