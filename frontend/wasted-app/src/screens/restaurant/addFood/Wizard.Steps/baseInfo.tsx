import React, {useEffect, useState} from "react"
import {StyleSheet} from "react-native"
import {Colors, Incubator, LoaderScreen, Picker, Text, TextArea, View} from "react-native-ui-lib"
const {TextField} = Incubator
import {TextFieldProps} from "react-native-ui-lib/generatedTypes/src/incubator"
import {Food, FoodType} from "../../../../api/interfaces"
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
  const [selectedTypes, setSelectedTypes] = useState<string[]>([])

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

  const onChangeTypeOfMeal = (typesOfMeal: string[]) => {
    setSelectedTypes(typesOfMeal)
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
    <View flexG center width="100%">
      <View centerV width={320}>
        <TextField
          textFieldCommonValues
          enableErrors
          validateOnChange
          marginT-s4
          marginB-s4
          fieldStyle={styles.withUnderline}
          label="Name *"
          value={baseInfo.name}
          validate="required"
          validationMessage="This field is required"
          validationMessagePosition={TextField.validationMessagePositions.BOTTOM}
          onChangeText={onChangeName}
        />
        <TextField
          textFieldCommonValues
          enableErrors
          validateOnStart
          validateOnChange
          marginT-s4
          marginB-s4
          fieldStyle={styles.withUnderline}
          label="Price *"
          value={String(baseInfo.currentPrice)}
          validate={["number", "required"]}
          validationMessage="This field is required"
          validationMessagePosition={TextField.validationMessagePositions.BOTTOM}
          onChangeText={onChangePrice}
        />
        <TextField
          textFieldCommonValues
          enableErrors
          validateOnChange
          marginT-s4
          marginB-s4
          fieldStyle={styles.withUnderline}
          label="URL of the image"
          validate="url"
          value={baseInfo.imageURL}
          validationMessage="It must be an URL"
          validationMessagePosition={TextField.validationMessagePositions.BOTTOM}
          onChangeText={onChangeUrl}
        />
        <Picker
          showSearch
          customPickerProps={{padding: 10}}
          style={{borderColor: Colors.yellow10}}
          placeholder="Type of meal"
          searchPlaceholder={"Search for a type of meal"}
          mode="MULTI"
          getLabel={(items: FoodType[]) => {
            let string = ""
            items.forEach(item => (string += item + ", "))
            return string.slice(0, string.length - 2)
          }}
          value={selectedTypes}
          onChange={onChangeTypeOfMeal}
        >
          {loading ? (
            <LoaderScreen />
          ) : (
            typesOfFood.map(option => <Picker.Item label={option.name} key={option.id} value={option.name} />)
          )}
        </Picker>
        <Text>Hello</Text>
        <Incubator.TextField
          marginB-s6
          paddingT-s2
          paddingH-s2
          multiline
          showCharCounter
          maxLength={200}
          label="Description (optional)"
          fieldStyle={{borderColor: Colors.blue60, borderWidth: 1, height: 100}}
          onChangeText={onChangeDescription}
        />
      </View>
    </View>
  )
}

const styles = StyleSheet.create({
  withUnderline: {
    borderBottomWidth: 1,
    borderColor: Colors.blue60,
    paddingBottom: 4
  }
})
