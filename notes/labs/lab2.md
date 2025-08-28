# THE FINAL LAB

## "Required"

Please do the lab called "design-sprint.md" in this same folder.

## Options

### Redo the Lab1 

You can follow the same instructions - Just do it again.

### Testing

Using the "starter" for the tests I have in the `src/HelpDesk/HelpDeskSolution/HelpDesk.Vip.Api.Tests/`, add tests for the VIP endpoints:

1. Adding Two VIPs with the same "sub" returns a 409 (Conflict).
2. Adding a VIP without a `sub` or a `description` that is 10-500 characters long returns a 400.
3. You can delete a VIP.