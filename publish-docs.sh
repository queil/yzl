#!/bin/bash
# if it fails make sure chown root:$USER ~/.ssh/config
./build-fsdocs.sh "https://queil.github.io/yzl/"
docker run --rm -it -v ~/.ssh:/root/.ssh:Z -v ${PWD}:/docs:Z docker.io/squidfunk/mkdocs-material gh-deploy
