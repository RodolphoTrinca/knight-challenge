import type IKnight from "@/interfaces/IKnight";

const API_URL = "http://localhost:10000/api"

async function fetchData<T>(url: string) {
    const response = await fetch(url);
    return response.json() as T;
}

export async function getKnights() {
    return fetchData<IKnight[]>(API_URL + "/HallKnights");
}

export async function getHeroes() {
    return fetchData<IKnight[]>(API_URL + "/HallKnights?filter=heroes");
}