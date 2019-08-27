#!/bin/sh

sudo apt-get update
sudo apt-get install make

make build-project

make run-tests