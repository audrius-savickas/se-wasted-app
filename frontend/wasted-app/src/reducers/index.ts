import {combineReducers} from "redux"
import {authentication} from "./authentication"
import {customer} from "./customer"
import {location} from "./location"
import {restaurant} from "./restaurant"

export const reducer = combineReducers({restaurant, location, authentication, customer})
