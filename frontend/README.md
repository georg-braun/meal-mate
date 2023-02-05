# Meal Mate

Supports you with your meal planning and your daily shopping. 

---

I didn't want to repeat the following things regarding the meal plan and shopping list every one to two weeks:
- "Which ingredients should be available and which planned meals can I cook with them?"
- "Hmm.. what I gonna eat this or next week? What do I like?"
- "What ingredients do I need for the planned meals?"
- Then I have to check the recipes and add the items to my shopping list

This procedure consumes time that I rather spend somewhere elese. Therefore I create a little app that helps me with the following stuff: 

🚲 Basic 
- [ ] Show me shopping lists with the stuff that I have to buy somewhere.
  - [x] Associate the shopping list with an user
  - [x] Subscriptions only to lists that are connected to the logged in user
  - [x] I can add manually add items to the list.
  - [x] I can check off items on the shopping list during shopping.
  - [x] Show the category of the shopping list entry.
  - [x] Sort the shopping list by category therefore I don't have to run zigzag through the supermarket.

- [ ] Manage the meals I like with the corresponding ingredients.
  - [ ] I can select a meal and add the ingredients to the shopping list.

Refactoring ideas:
- Make a basic generic repository with the CRUD methods and derive specializer repositories. The benefit could be the removal of the `type` property (in `BaseItem`) with the class name.

🚀 Nice to have:

- [ ] Manage a meal plan with the meals for the next one to two weeks
- [ ] Share a shopping list with other user

(Feature List)

---

ToDo List:

- [ ] Add shopping list entries
- [ ] Check off an shopping list entry 
- [ ] Sort the shopping list items by category
- [ ] Associate shopping list with an user
- [ ] Only send shopping list updates to an with the list connected user
- [ ] Change the category of an item
- [ ] Add recipes with ingredients