import React, {useEffect, useState} from "react"
import {Text, Incubator, TextArea, Picker, View, LoaderScreen} from "react-native-ui-lib"
const {TextField} = Incubator
import {TextFieldProps} from "react-native-ui-lib/generatedTypes/src/incubator"
import {FoodType} from "../../../../api/interfaces"
import {getAllTypesOfFood} from "../../../../api/type-of-food"
import {Props} from "./interfaces"

const textFieldCommonValues: TextFieldProps = {
  editable: false,
  centered: false
}

interface IBaseInfo {
  name: string
  description: string
  imageURL: string
  currentPrice: number
}

export const BaseInfo = ({food, setFood}: Props) => {
  const [loading, setLoading] = useState(true)
  const [typesOfFood, setTypesOfFood] = useState<FoodType[]>([])

  const [baseInfo, setBaseInfo] = useState<IBaseInfo>({
    name: food.name,
    currentPrice: food.currentPrice,
    imageURL: food.imageURL,
    description: food.description
  })

  useEffect(() => {
    fetchFoods()
  }, [])

  const onChangeName = (name: string) => {
    setBaseInfo({...baseInfo, name})
    setFood({...food, name})
  }

  const onChangePrice = (currentPrice: string) => {
    let price: number = Number(currentPrice)
    if (isNaN(price)) price = 0

    setBaseInfo({...baseInfo, currentPrice: price})
    setFood({
      ...food,
      currentPrice: price
    })
  }

  const onChangeUrl = (imageURL: string) => {
    setBaseInfo({...baseInfo, imageURL})
    setFood({...food, imageURL})
  }

  const onChangeDescription = (description: string) => {
    setBaseInfo({...baseInfo, description})
    setFood({...food, description})
  }

  const fetchFoods = async () => {
    const response = await getAllTypesOfFood()
    setTypesOfFood(response)
    setLoading(false)
  }

  const onChangeTypeOfMeal = (typesOfMeal: string) => {
    const selectedTypes: FoodType[] = []
    typesOfFood.map(typeOfFood => {
      if (typesOfMeal.includes(typeOfFood.name)) selectedTypes.push(typeOfFood)
    })
    setFood({
      ...food,
      typesOfFood: selectedTypes
    })
  }

  return (
    <View
      flex
      style={{
        flexDirection: "column"
      }}
    >
      <TextField
        label="Name *"
        value={baseInfo.name}
        onChangeText={onChangeName}
        validate="required"
        validationMessage="This field is required"
        textFieldCommonValues
        enableErrors
        validateOnStart
        validateOnChange
        validationMessagePosition={TextField.validationMessagePositions.BOTTOM}
        marginT-s4
        marginB-s4
      />
      <TextField
        label="Price *"
        value={String(baseInfo.currentPrice)}
        onChangeText={onChangePrice}
        validate={["number", "required"]}
        validationMessage="This field is required"
        textFieldCommonValues
        enableErrors
        validateOnStart
        validateOnChange
        validationMessagePosition={TextField.validationMessagePositions.BOTTOM}
        marginT-s4
        marginB-s4
      />
      <TextField
        label="Url of the image"
        validate="url"
        value={baseInfo.imageURL}
        onChangeText={onChangeUrl}
        validationMessage="It must be an url"
        textFieldCommonValues
        enableErrors
        validateOnChange
        validationMessagePosition={TextField.validationMessagePositions.BOTTOM}
        marginT-s4
        marginB-s4
      />
      <Picker
        placeholder="Type of meal"
        showSearch
        searchPlaceholder={"Search a type of meal"}
        onChange={onChangeTypeOfMeal}
        mode="MULTI"
      >
        {loading ? (
          <LoaderScreen />
        ) : (
          typesOfFood.map(option => <Picker.Item label={option.name} key={option.id} value={option.name} />)
        )}
      </Picker>
      <Text>Description</Text>
      <TextArea value={baseInfo.description} onChangeText={onChangeDescription} />
    </View>
  )
}
