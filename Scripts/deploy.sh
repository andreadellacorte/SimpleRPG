#!/usr/bin/env bash

set -e
set -u

spatial cloud upload $ASSEMBLY_NAME

spatial cloud launch $ASSEMBLY_NAME default_launch.json $PROJECT_NAME --snapshot=snapshots/default.snapshot --cluster_region=eu
