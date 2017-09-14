#!/usr/bin/env bash

set +e

project_name=MySimpleRPG
assembly_name=$project_name
assembly_name+=_$( date +%Y%m%d_%H%M%S )

spatial worker build --target=deployment

set -e

spatial cloud upload $assembly_name

spatial cloud delete $project_name

spatial cloud launch $assembly_name default_launch.json $project_name --snapshot=snapshots/default.snapshot --cluster_region=eu
