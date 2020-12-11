---
title: "yzl - YAML Zero-Language - F# DSL for YAML"
---

[Yzl](https://github.com/queil/yzl) (pronounced *easel*) stands for YAML Zero-Language. It's a pragmatic template-less approach to YAML generation powered by F#.



## Building docs

The docs are built using MkDocs and API Reference is generated with `FSharp.Formatting.CommandTool`. At present the process is experimental and uses my fork of [FSharp.Formatting](https://github.com/queil/FSharp.Formatting).

Generating API docs:

```
dotnet run --no-build -p ~/github/FSharp.Formatting/src/FSharp.Formatting.CommandTool/ -- build --nodefaultcontent --projects ./src/Yzl.fsproj  --output ./docs --input ./fsdocs --parameters root /
```

## Publish docs

```
docker run -p 8089:8089 --rm -it -v ~/.ssh:/root/.ssh -v ${PWD}:/docs squidfunk/mkdocs-material gh-deploy
```

## Work with docs
```
docker run -p 8089:8089 --rm -it -v ~/.ssh:/root/.ssh -v ${PWD}:/docs \
  squidfunk/mkdocs-material serve -a 0.0.0.0:8089
```