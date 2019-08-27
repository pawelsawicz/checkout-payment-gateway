#!/bin/sh

apt-get update
apt-get install make

make build-project

make run-tests