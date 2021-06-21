# Problem Solving
This is a unit-test project for practicing solving Olympiad problems. Most of the tasks are picked from [LeetCode](https://leetcode.com/explore/).

## How to arrange a solution
Example is provided in [00001_ExampleProblem](./00001_ExampleProblem.cs)

- Firstly, create a new class - it has to be named in such a way `id_NameOfTask`
- Derive class from [IOlympiadTask](./IOlympiadTask.cs): there is a single method `void Solve()`
- Mark a class with `[TestClass]` attribute. Method `Solve()` has to be marked with `[TestMethod]` attribute as well. I'm using [Shoudly](https://github.com/shouldly/shouldly) for checking results of methods in unit-tests. If you want to use other similar library - feel free to use another (but not super complex one)
- Right above the name of class, please, create a multi-row comment with description of task. This is done for not searching a task description in LeetCode - all information could be found in a single file. Template: 
```
/*

Task:
-----

Solution Description:
-----

*/
```

- Create a private method with a signature, provided by LeetCode. Test your solution in `Solve()`. That's it :)