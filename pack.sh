pushd "`dirname \"$0\"`/.."
version=`grep "version =" PayUSharp/PayUSharp.sln | awk '{print $3}' | tr -d ' \r\n'`
output=PayUSharp-v$version.7z
rm $output
7za a -r -x!.git -x!*.mdb -x!pack.sh -x!*.txt -x!*.md -x!*.pdf -x!.DS_Store -x!*.userprefs -x!Debug -x!obj $output PayUSharp/*
popd