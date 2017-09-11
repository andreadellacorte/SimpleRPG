#! /bin/sh

spatial diagnose

set +e

mkdir -p ~/.improbable/oauth2
mv ./secret ~/.improbable/oauth2/oauth2_refresh_token

mv /Applications/Unity-5.6.1f1 /Applications/Unity

assembly_name=MySimpleRPG_$( date +%Y%m%d_%H%M%S )

spatial worker build --target=deployment

spatial cloud upload $assembly_name

spatial cloud launch $assembly_name default_launch.json beta_batman_crazy_339 --snapshot=snapshots/default.snapshot --cluster_region=eu

set -e