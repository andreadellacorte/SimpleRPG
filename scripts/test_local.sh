#!/usr/bin/env bash

set -u

gtimeout 45s spatial local launch

status=$?

if [ $status -eq 124 ] #timed out
then
    exit 0
fi
exit $status
