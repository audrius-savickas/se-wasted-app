import React, {useEffect, useState} from "react"
import {StyleSheet} from "react-native"
import {Colors, Incubator, LoaderScreen, Picker, View} from "react-native-ui-lib"
import {FoodType} from "../../../../api/interfaces"
import {getAllTypesOfFood} from "../../../../api/type-of-food"
import {Props} from "./interfaces"

const {TextField} = Incubator
interface IBaseInfo {
  name: string
  description: string
  imageURL: string
  currentPrice: string
}

export const BaseInfo = ({food, setFood}: Props) => {
  const [loading, setLoading] = useState(true)
  const [typesOfFood, setTypesOfFood] = useState<FoodType[]>([])
  const [selectedTypes, setSelectedTypes] = useState<string[]>([])

  const [baseInfo, setBaseInfo] = useState<IBaseInfo>({
    name: food.name,
    currentPrice: food.currentPrice.toString(),
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
    setBaseInfo({...baseInfo, currentPrice})
    setFood({
      ...food,
      currentPrice: Number(currentPrice)
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
          value={baseInfo.currentPrice.toString()}
          validate={["number", "required"]}
          validationMessage={["Invalid number", "This field is required"]}
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
          onChangeText={onChangeUrl}
        />
        <Picker
          showSearch
          multiline
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
