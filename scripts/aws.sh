#!/bin/sh
sleep 10s
aws --endpoint-url=http://localstack:4572 s3api create-bucket --bucket rawbucket
tail -f /dev/null