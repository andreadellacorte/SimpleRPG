#!/usr/bin/env bash

set -e

brew tap caskroom/cask
brew tap improbable-io/spatialos
brew update

brew cask install spatial

if [ ! -d "$UNITY_FOLDER"/Unity.app ]; then
  brew cask install unity@$UNITY_VERSION
  brew cask install unity-linux-support-for-editor@$UNITY_VERSION
  brew cask install unity-standard-assets@$UNITY_VERSION
  brew cask install unity-windows-support-for-editor@$UNITY_VERSION
fi
