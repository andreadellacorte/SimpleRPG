#!/usr/bin/env bash

set -e
set -u

openssl aes-256-cbc -K $encrypted_3a211ca5956d_key -iv $encrypted_3a211ca5956d_iv -in secret.enc -out secret -d
mkdir -p ~/.improbable/oauth2
mv ./secret ~/.improbable/oauth2/oauth2_refresh_token
