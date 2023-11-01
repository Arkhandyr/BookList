export interface Book {
	_id: string
	cover: string;
	title: string;
	author: string;
	pages: number;
	synopsis: string;
	genre: string;
	publisher: string;
	year: number
	interactionData: InteractionData
}

export interface InteractionData {
	planning: number,
	reading: number,
	done: number
}