#!/bin/bash

./build-fsdocs.sh "https://queil.github.io/yzl/"
docker run -p 8089:8089 --rm -it -v ~/.ssh:/root/.ssh -v ${PWD}:/docs squidfunk/mkdocs-material gh-deploy
