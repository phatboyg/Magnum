@echo off

@echo Environment details
ruby --version

@call bundle -v
IF ERRORLEVEL 1 (@call gem install bundler)

@call bundle install

echo Building for .NET 3.5
call bundle exec rake BUILD_CONFIG_KEY=NET35 %*
IF NOT %ERRORLEVEL% == 0 goto FAILED

echo Building for .NET 4.0
call bundle exec rake unclean %*
IF NOT %ERRORLEVEL% == 0 goto FAILED

echo Create ZIP package
call bundle exec rake package %*

:FAILED
