#! /bin/sh

tar xvf secrets.tar

mkdir -p ~/.m2
mv settings.xml ~/.m2/settings.xml

mkdir -p ~/.ivy2
mv improbable.credentials ~/.ivy2/improbable.credentials

spatial update

spatial worker build --target=deployment

spatial cloud upload MySimpleRPG --log_level=debug

spatial cloud launch MySimpleRPG default_launch.json beta_batman_crazy_339 --snapshot=snapshots/default.snapshot --cluster_region=eu
