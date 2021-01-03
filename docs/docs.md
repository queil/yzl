# Working with docs

## Publish

```
./publish-docs.sh
```

## Serve locally
```
./serve-docs.sh
```

## Building API docs

The docs are built using MkDocs and API Reference is generated with `FSharp.Formatting.CommandTool`. At present the process is experimental and uses my fork of [FSharp.Formatting](https://github.com/queil/FSharp.Formatting).

Generating API docs:

```
./build-fsdocs.sh ${ROOT_URL}
```
