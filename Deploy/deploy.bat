@echo off
python deploytool.py config.json SliceOfPiAuto create-db
python deploytool.py config.json SliceOfPiAuto create-web-app
pause