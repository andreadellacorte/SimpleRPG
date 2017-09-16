#!/usr/bin/env bash

set -e

spatial cloud delete $PROJECT_NAME || true

spatial cloud upload $ASSEMBLY_NAME

spatial cloud launch $ASSEMBLY_NAME default_launch.json $PROJECT_NAME --snapshot=snapshots/default.snapshot --cluster_region=eu
