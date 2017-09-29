#!/usr/bin/env bash

set -e
set -u

spatial cloud delete $DEPLOYMENT_NAME || true
