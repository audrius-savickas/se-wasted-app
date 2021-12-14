import {combineReducers} from "redux"
import {authentication} from "./authentication"
import {location} from "./location"
import {restaurant} from "./restaurant"
import {customer} from "./user"

export const reducer = combineReducers({restaurant, location, authentication, customer})
