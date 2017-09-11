#! /bin/sh

mkdir -p ~/.improbable/oauth2
mv ./secret ~/.improbable/oauth2/oauth2_refresh_token

spatial update

spatial worker build --target=deployment

spatial cloud upload MySimpleRPG --log_level=debug

spatial cloud launch MySimpleRPG default_launch.json beta_batman_crazy_339 --snapshot=snapshots/default.snapshot --cluster_region=eu
