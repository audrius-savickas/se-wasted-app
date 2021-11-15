import {Dispatch, SetStateAction} from "react"
import {Food} from "../../../../api/interfaces"

export interface Props {
  food: Food
  setFood: Dispatch<SetStateAction<Food>>
}
