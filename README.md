# TextGS

## Table of Contents  
- Task description
- Assumptions
- Ensureness of sentences repeatability
- String transformation
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
1. Generated file size and chunk sizes should be set in Gb.
2. Encoding of the generated file is UTF8.
3. Dictionary for the words to make sentences (String) is defined in a separate file.
4. Sentence max length is 16 words.
5. First word of sentence starts with capital letter.
6. Numbers (Number) are positive integers.

## Ensureness of sentences repeatability
Dictionary of max size 1024 is created in order to store sentences which could be repeated. As assumed the size of the generated file ~100Gb, the parameters are tuned in the way to ensure repeatability of the sentences. For smaller size of the generated file the parameters should be tuned in different way (`_repeatSpreading` parameter in `WordsGenerator.cs`).

## String transformation
`Number. String` is not a good format to compare. Thus it is transformed to `String PaddedNumber` format to ensure more efficient comparison. For instance, if the string before transformation is `32. Cherry is the best`, after transformation it becomes `Cherry is the best 0000000032`. Padding is defined based on integer digits count. 

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
