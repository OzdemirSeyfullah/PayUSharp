#~/bin/bash -e
version=`grep "version =" PayUSharp.sln | awk '{print $3}' | tr -d ' \r\n'`
output=dist/PayUSharp-v$version.7z


pushd "`dirname \"$0\"`" >> /dev/null
mkdir -p "dist"

rm "$output"

# Build the project
xbuild /nologo /verbosity:minimal /property:Configuration=Release PayUSharp.sln

# Update documentation
mono util/bin/Doc.exe docs

# Generate documentation
pushd "docs" >> /dev/null
echo "Generating PDF documentation"
./generate.sh
echo "  PDF documentation generated"
popd >> /dev/null

# Pack all the bin and the src everything
7za a -xr@.packignore $output src bin/Release docs/*.pdf | grep --color=never "ing archive"
popd >> /dev/null
