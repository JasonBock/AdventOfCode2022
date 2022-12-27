1 2 3 4 5 6 7 8 9

Move 2 to the right 3 places, it should be:

1 3 4 5 2 6 7 8 9

Swapping takes too long when the numbers get bigger

Linked list feels like the right answer here, because we could say, "1 links to 3, 5 links to 2, and 2 links to 6", and be done with it. But then how would I traverse it?

0 1 2 3 4 8 6

NOT INCLUDING ITSELF!


0 1 2 3 4 24 6

0 1 2 3 4 -10 6

0 -10 1 2 3 4 6