# TextGS

## Table of Contents  
- Task description
- Assumptions
- How to run

## Task description

The input is a large text file, where each line is a Number. String
For example:
```
415. Apple
30432. Something something something
1. Apple
32. Cherry is the best
2. Banana is yellow
```

Both parts can be repeated within the file. You need to get another file as output, where all the lines are sorted. Sorting criteria: String part is compared first, if it matches then Number. Those in the example above, it should be:
```
1. Apple
415. Apple
2. Banana is yellow
32. Cherry is the best
30432. Something something something
```

You need to write two programs:
1. A utility for creating a test file of a given size. The result of the work should be a text file of the type described above. There must be some number of lines with the same String part.
2. The actual sorter. An important point, the file can be very large. The size of ~100Gb will be used for testing.
When evaluating the completed task, we will first look at the result (correctness of generation / sorting and running time), and secondly, at how the candidate writes the code. Programming language: C#.

## Assumptions
1. Generated file size and chunk sizes should be defined in Gb.
2. Generated file's encoding should be UTF8.
3. Dictionary for the words to make sentences (String) is defined in the separate file, one word per row.
4. Numbers are positive integers.

## How to run
1. Text Generator: `dotnet run -c Release --project .\TextGenerator.csproj -- "D:/Repos/TextGS/generated_file.txt" "D:/Repos/TextGS/dictionary.txt" 100`, where
```
arg[0] -> generated file path
arg[1] -> dictionary path
arg[2] -> generated file size
```
2. Text Sorter: `dotnet run -c Release --project .\TextSorter.csproj -- 3 "D:/Repos/TextGS/Chunks/" "D:/Repos/TextGS/generated_file.txt" "D:/Repos/TextGS/sorted_file.txt"`, where
```
arg[0] -> chunk size
arg[1] -> chunk files path
arg[2] -> generated file path
arg[3] -> output sorted file path
```
