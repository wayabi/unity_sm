#!/bin/sh

name=$1
csv_stats=$2
rm -fR out/*
cp -R data/State ./out
mkdir ./out/${name}State
./a.out ${name} ${csv_stats}
