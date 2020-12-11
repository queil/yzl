#!/bin/bash

./build-fsdocs "http://localhost:8089/"
docker run -p 8089:8089 --rm -it -v ~/.ssh:/root/.ssh -v ${PWD}:/docs squidfunk/mkdocs-material serve -a 0.0.0.0:8089
