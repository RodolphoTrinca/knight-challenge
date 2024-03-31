import type IKnight from "@/interfaces/IKnight";

export async function getKnights() {
    return [
        {
            name: 'knight 1',
            age: 39,
            weapons: 2,
            attribute: 'strenght',
            attack: 20,
            experience: 6000
        },
        {
            name: 'knight 2',
            age: 21,
            weapons: 1,
            attribute: 'dexterity',
            attack: 3,
            experience: 120
        },
        {
            name: 'knight 3',
            age: 7,
            weapons: 8,
            attribute: 'constitution',
            attack: 60,
            experience: 459
        },
        {
            name: 'knight 4',
            age: 31,
            weapons: 1,
            attribute: 'wisdom',
            attack: 57,
            experience: 28000
        },
    ]
}