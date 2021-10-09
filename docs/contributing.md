---
title: "Yzl - Contributing"
---

## Working with docs

### Publish

```
./publish-docs.sh
```

### Serve locally
```
./serve-docs.sh
```

### Building API docs

The docs are built using MkDocs and API Reference is generated with [fsdocs](https://fsprojects.github.io/FSharp.Formatting/commandline.html).

Generating API docs:

```
./build-fsdocs.sh ${ROOT_URL}
```
