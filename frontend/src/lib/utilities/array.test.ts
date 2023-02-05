import { assert, expect, test } from 'vitest'
import { removeEntryWithId, sortByCategory } from './array';

test("correct element is removed", () => {
    // arrange
    const data = [{id: 1},{id: 2},{id: 3}];

    // act
    const modifiedData = removeEntryWithId(data, 2)


    // assert
    expect(modifiedData).toStrictEqual([{id: 1},{id: 3}])
})

test("sort by categoryId", () => {
    // arrange
    const data = [{id: 1, category: "1"},{id: 2, category: "2"},{id: 3, category: "1"}];

    // act
    const sorted = sortByCategory(data)


    // assert
    expect(sorted).toStrictEqual([{id: 1, category: "1"},{id: 3, category: "1"},{id: 2, category: "2"}])
})