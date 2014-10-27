pushd "`dirname \"$0\"`"
mkdir -p "dist"
version=`grep "version =" PayUSharp.sln | awk '{print $3}' | tr -d ' \r\n'`
output=dist/PayUSharp-v$version.7z
rm $output
7za a -r -x@.packignore $output ./src/*
popd
