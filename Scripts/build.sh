#!/usr/bin/env bash

set -e

spatial worker build --target=deployment

spatial cloud upload $ASSEMBLY_NAME

spatial cloud delete $PROJECT_NAME || true

spatial cloud launch $ASSEMBLY_NAME default_launch.json $ASSEMBLY_NAME --snapshot=snapshots/default.snapshot --cluster_region=eu
