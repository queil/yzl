#!/bin/bash

./build-fsdocs.sh "https://queil.github.io/yzl/"
#podman: docker run -p 8089:8089 --rm -it -v ~/.ssh:/root/.ssh:Z -v ${PWD}:/docs:Z docker.io/squidfunk/mkdocs-material gh-deploy
docker run -p 8089:8089 --rm -it -v ~/.ssh:/root/.ssh:ro -v ${PWD}:/docs docker.io/squidfunk/mkdocs-material gh-deploy
