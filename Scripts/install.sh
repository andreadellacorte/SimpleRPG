#!/usr/bin/env bash

set -e

mkdir -p ~/.improbable/oauth2
mv ./secret ~/.improbable/oauth2/oauth2_refresh_token

brew tap caskroom/cask
brew tap improbable-io/spatialos
brew update

brew cask install spatial

brew cask install unity@$UNITY_VERSION
brew cask install unity-linux-support-for-editor@$UNITY_VERSION
brew cask install unity-standard-assets@$UNITY_VERSION
brew cask install unity-windows-support-for-editor@$UNITY_VERSION
