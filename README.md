# Glossary

Due to having to work on an old Mac laptop, this project is built targetting .NET Core 2.2.8, and using Angular CLI version 8.3.19, and SCSS as stylesheet preprocessor.

Favicon Icon made by [smalllikeart](https://www.flaticon.com/authors/smalllikeart) from www.flaticon.com.

### Problem

A client requires a simple glossary system whereby individual terms and a corresponding definition can be persisted into a data store and later retrieved.

The entities are defined as:

Term – a single word or short phrase that is the term

Definition – a paragraph of text that defines the term

Editors in the system must be able to:
* add a new term and definition to the system
* edit an existing term and definition
* remove terms from the system
* view an alphabetically-sorted list of the terms and definitions

### Possible bugs / Todos
-Data integrity/validation, and make sure that terms are constrained uniquely
