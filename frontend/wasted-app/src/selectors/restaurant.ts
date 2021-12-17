import {State} from "../typings/redux"

export const getRestaurantId = (state: State) => state.restaurant.id

export const getRestaurant = (state: State) => state.restaurant.restaurant
