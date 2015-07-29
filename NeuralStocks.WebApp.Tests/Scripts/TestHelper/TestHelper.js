/// <reference path="sinon-1.15.4.js" />
/// <reference path="../../../neuralstocks.webapp/scripts/external/jquery-1.10.2.js" />
/// <reference path="../jasmine/jasmine.js" />

function assertEquals(expected, actual) {
    expect(actual).toEqual(expected);
}

function assertFalse(condition) {
    expect(condition).toBeFalsy();
}

function assertTrue(condition) {
    expect(condition).toBeTruthy();
}
