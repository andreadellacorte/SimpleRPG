#!/usr/bin/env bash

set -e
set -u

gtimeout -s SIGINT 45s spatial local launch
