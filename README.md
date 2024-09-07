# StateSmith bot

This bot contains a git subtree-merged version of [the main StateSmith repository](https://github.com/StateSmith/StateSmith).

To merge in latest changes, run these commands:

```shell
git remote add -f statemith https://github.com/StateSmith/StateSmith.git
git subtree pull --prefix=statesmith statemith main --squash
```
