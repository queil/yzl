---
title: "yzl - YAML Zero-Language - F# DSL for YAML"
---

[Yzl](https://github.com/queil/yzl) (pronounced *easel*) stands for YAML Zero-Language. It's a pragmatic template-less approach to YAML generation powered by F#.



## Building docs

The docs are built using MkDocs and API Reference is generated with `FSharp.Formatting.CommandTool`. At present the process is experimental and uses my fork of [FSharp.Formatting](https://github.com/queil/FSharp.Formatting).

Generating API docs:

```
./build-fsdocs.sh ${ROOT_URL}
```

## Publish docs

```
./publish-docs.sh
```

## Work with docs
```
./serve-docs.sh
```