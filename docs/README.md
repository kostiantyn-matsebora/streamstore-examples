# StreamStore Examples

Following repository contains examples of using [`StreamStore`](https://github.com/kostiantyn-matsebora/streamstore) component with different types of persistence.

Each type of storage has its own example project, for instance, you can find an example of usage in the [StreamStore.Sql.Example](../src/StreamStore.Sql.Example) project.

Example projects provides a simple console application that demonstrates how to **configure and use** [`StreamStore`] in your application as single storage or multitenancy.

`Single storage` examples demonstrates:

- Concurrency control
- asynchronous reading and writing operations
  
`Multitenancy` examples, in turn, demonstrates asynchronous reading and writing operations in **isolated tenant storage**.

For getting all running options simply run the application with `--help` argument.

For configuring application via configuration file, create `appsettings.Development.json` file.

## License

[`MIT License`](../LICENSE)

[`StreamStore`]: https://github.com/kostiantyn-matsebora/streamstore/
