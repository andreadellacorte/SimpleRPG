#!/usr/bin/env bash

set -e

project_name=mysimplerpg
assembly_name=$project_name$( date +%Y%m%d%H%M%S )

spatial worker build --target=deployment

spatial cloud upload $assembly_name

spatial cloud delete $project_name || true

spatial cloud launch $assembly_name default_launch.json $project_name --snapshot=snapshots/default.snapshot --cluster_region=eu
